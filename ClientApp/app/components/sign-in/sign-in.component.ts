import { Component } from '@angular/core';
import { LocalStorageService } from './../../services/local-storage.service';
import { HttpHelper } from './../../services/http-helper.service';
import { Router } from '@angular/router';
declare const gapi: any;
@Component({
    selector: 'sign-in',
	templateUrl: './sign-in.component.html',
	styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {

	public auth2: any;
	
	

	constructor(private httpHelper: HttpHelper, private localStorageService: LocalStorageService, private router: Router) { }

	public googleInit() {
		
		gapi.load('auth2', () => {
			this.auth2 = gapi.auth2.init({
				client_id: '1035056266350-q0gdfpp1jn8eeei0qs6l0dlrmv2l5jpp.apps.googleusercontent.com'
			});
			this.attachSignin(document.getElementById('googleBtn'));
		});
		
	}
	public singin()
	{
		let profile = this.auth2.getBasicProfile();
		this.httpHelper.Post("api/account/signin", { "Email": profile.getEmail() }).
			subscribe(res => {
				var obj = JSON.parse(res.text());
				this.localStorageService.authToken = obj['auth_token'];
				this.router.navigate(['main']);

			});
	}
	public attachSignin(element: any) {
		this.auth2.attachClickHandler(element, {},
			(googleUser: any) => {
				let profile = googleUser.getBasicProfile();
				
				this.httpHelper.Post("api/account/signin", { "Email": profile.getEmail() }).
					subscribe(res => {							
					var obj = JSON.parse(res.text());					
					this.localStorageService.authToken = obj['auth_token'];
					
					
				});


			}, (error: any) => {
				console.log(JSON.stringify(error, undefined, 2));
			});
	}
	public LogOut() {	
		this.localStorageService.authTokenClean();
		
	}
	
	ngAfterViewInit() {
		this.googleInit();
		
	}
}
