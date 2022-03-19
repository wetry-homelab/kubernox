import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { AuthenticationRequest, PasswordUpdateRequest } from "../contracts/request";
import { AuthenticationResponse, CoreResponse } from "../contracts/response";
import { BehaviorSubject, Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { User } from "../models/user.model";

@Injectable({ providedIn: 'root' })
export class IdentityService {

    private currentUserSubject!: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private httpClient: HttpClient) {
        var user = localStorage.getItem('currentUser');
        if (user) {
            this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(user));
        }

        this.currentUser = this.currentUserSubject?.asObservable();
    }


    public get currentUserValue(): User {
        return this.currentUserSubject?.value;
    }

    public authenticateUserAsync(request: AuthenticationRequest): Observable<CoreResponse<AuthenticationResponse>> {
        return this.httpClient.post<CoreResponse<AuthenticationResponse>>(`${environment.baseServiceUri}/identity`, request);
    }

    public updatePasswordAsync(request: PasswordUpdateRequest): Observable<CoreResponse<AuthenticationResponse>> {
        return this.httpClient.post<CoreResponse<AuthenticationResponse>>(`${environment.baseServiceUri}/identity/password-update`, request);
    }
}