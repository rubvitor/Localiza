import { Injectable } from '@angular/core';
import { HttpService } from 'app/core/http-services.service';
import { ValidationResultModel } from 'app/models/validation-result';
import { environment } from 'environments/environment';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from 'app/core/config/config.service';

@Injectable({
  providedIn: 'root',
})
export class NumeroService extends HttpService {

  public codigoDivisorPrimoModal: string;

  constructor(http: HttpClient,
              config: ConfigService) {
    super(http, config);
  }

  getDivisorPrimos(numero: number, traceId: string) {
    const url = `${environment.baseApi}/numero-management/${numero}/${traceId}`;
    return super.get<ValidationResultModel>(url);
  }
}
