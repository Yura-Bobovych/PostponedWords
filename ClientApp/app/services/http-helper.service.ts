import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions} from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { LocalStorageService } from './local-storage.service';
@Injectable()
export class HttpHelper {
	constructor(private http: Http, private localStorageService: LocalStorageService) { }
	Get(path: string, data?: object, ): Observable<Response>
	{
		let headers = this.GetHeadersAppJson();

		return this.http.get(path, { headers, params: data });
	}
	Post(path: string, data: object): Observable<Response>
	{
		let headers = this.GetHeaders();
		let options = new RequestOptions({ headers: headers });
		return this.http.post(path, data, options);
	}
	GetAuthorize(path: string, data?: object): Observable<Response> {
		let headers = this.GetHeaders();
		return this.http.get(path, { headers, params: data });
	}

	PostAuthorize(path: string, data: object): Observable<Response>
	{
		
		let headers = this.GetHeaders();
		let options = new RequestOptions({ headers: headers });
		return this.http.post(path, data, options)
		
	}
	private GetHeaders(): Headers
	{
		let headers = new Headers();
		headers.append('Content-Type', 'application/json');
		headers.append('Authorization', `Bearer ${this.localStorageService.authToken}`);
		console.log(this.localStorageService.authToken);
		return headers;
	}
	private GetHeadersAppJson(): Headers
	{
		let headers = new Headers();
		headers.append('Content-Type', 'application/json');
		return headers;
	}
}