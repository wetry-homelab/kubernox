import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { CoreResponse } from "../contracts/response";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";


@Injectable({ providedIn: 'root' })
export class LogService {
    constructor(private httpClient: HttpClient) {
    }

    public getLogsAsync(): Observable<CoreResponse<any>> {
        return this.httpClient.get<CoreResponse<any>>(`${environment.baseServiceUri}/log`);
    }

}