import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';
import { UserModel } from 'app/models/userModel';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {

    public setToken(token: string) {
        sessionStorage.setItem('tokenUser', token);
    }

    public getToken(): string {
        return sessionStorage.getItem('tokenUser');
    }

    public removeToken() {
        sessionStorage.removeItem('tokenUser');
    }

    public getUser(): UserModel {
        const token = this.getToken();

        if (token && token !== '') {
            const jwt = jwt_decode<any>(token);
            if (jwt.user && jwt.user !== '') {
                return JSON.parse(jwt.user);
            } else {
                return null;
            }
        } else {
            return null;
        }
    }
}
