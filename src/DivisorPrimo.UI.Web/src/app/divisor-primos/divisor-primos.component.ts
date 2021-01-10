import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NumeroService } from 'app/services/numero.service';
import { DivisorPrimoModel } from 'app/models/divisor-primoModel';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from 'primeng/api';
import { environment } from 'environments/environment';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-divisor-primos',
  templateUrl: './divisor-primos.component.html',
  styleUrls: ['./divisor-primos.component.css']
})
export class DivisorPrimosComponent implements OnInit {
  numero?: number;
  divisorPrimoRetorno: DivisorPrimoModel;
  private hubConnection: HubConnection;
  msgs: Message[] = [];

  constructor(private toastr: ToastrService,
             private numeroService: NumeroService) { }

  calcularDivisorPrimo(e): void {
    if (e && e.keyCode !== 13) {
      return;
    }

    const traceId = Guid.create().toString();
    this.divisorPrimoRetorno = {
      numeroBase: this.numero,
      traceId: traceId
    };

    this.numeroService.getDivisorPrimos(this.numero, traceId).subscribe(res => {
      if (res.isValid) {
        if (!this.divisorPrimoRetorno) {
          this.divisorPrimoRetorno = {
            numeroBase: this.numero,
            traceId: traceId
          };
        }
      }
    });
  }

  ngOnInit() {
    const builder = new HubConnectionBuilder();
    this.hubConnection = builder.withUrl(`${environment.baseApi}/messagehub`).build();
    this.hubConnection.start();

    this.hubConnection.on('divisor', (traceId: string, numero: number) => {
      if (this.divisorPrimoRetorno && this.divisorPrimoRetorno.traceId === traceId) {
        if (!this.divisorPrimoRetorno.divisores) {
          this.divisorPrimoRetorno.divisores = [];
        }

        this.divisorPrimoRetorno.divisores.push(numero);
        this.divisorPrimoRetorno.divisores = this.divisorPrimoRetorno.divisores.sort((a, b) => { return a - b });
      }
    });

    this.hubConnection.on('primo', (traceId: string, numero: number) => {
      if (this.divisorPrimoRetorno && this.divisorPrimoRetorno.traceId === traceId) {
        if (!this.divisorPrimoRetorno.numerosPrimos) {
          this.divisorPrimoRetorno.numerosPrimos = [];
        }

        this.divisorPrimoRetorno.numerosPrimos.push(numero);
        this.divisorPrimoRetorno.numerosPrimos = this.divisorPrimoRetorno.numerosPrimos.sort((a, b) => { return a - b });
      }
    });
  }
}
