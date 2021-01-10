import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { UserModel } from '../../models/userModel';
import { environment } from 'environments/environment';
import { LoginModel } from 'app/models/loginModel';
import { ConfigService } from './config.service';
import { HttpService } from '../http-services.service';

@Injectable({ providedIn: 'root' })
export class AuthenticationService extends HttpService {
    private currentUserSubject: BehaviorSubject<UserModel>;
    public currentUser: Observable<UserModel>;

    constructor(public http: HttpClient,
                public configService: ConfigService) {
        super(http, configService);

        const userToken = configService.getUser();

        if (userToken && userToken !== null) {
            this.currentUserSubject = new BehaviorSubject<UserModel>(userToken);
            this.currentUser = this.currentUserSubject.asObservable();
        } else {
            this.currentUserSubject = new BehaviorSubject<UserModel>(null);
            this.currentUser = this.currentUserSubject.asObservable();
        }
    }

    public get currentUserValue(): UserModel {
        return this.currentUserSubject.value;
    }

    login(username, password) {
        const userModel: LoginModel = {
            email: username,
            password: password
        }
        return super.post<any>(`${environment.baseApi}/api/account/login`, userModel);
    }

    logout() {
        this.configService.removeToken();
        this.currentUserSubject.next(null);
    }
}
