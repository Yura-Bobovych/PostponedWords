import { Injectable, Inject } from '@angular/core';
import { LocalStorage } from './../models/local-storage'

@Injectable()
export class LocalStorageService {	
	constructor(
		@Inject(LocalStorage) private localStorage: any) { }
	public get authToken(): string {
	var data = this.localStorage.getItem('auth_token');
		return ((data)? data:"");		
		}
	public set authToken(value: string) {
		
		localStorage.setItem('auth_token', value);
		console.log(value + "   " + this.localStorage.getItem('auth_token'));
	 
	}
	public authTokenClean() { this.localStorage.removeItem('auth_token');}
	
}