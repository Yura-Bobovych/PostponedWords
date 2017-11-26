import { Component } from '@angular/core';
import { LocalStorageService } from './../../services/local-storage.service';
import { HttpHelper } from './../../services/http-helper.service';

@Component({
    selector: 'main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.css']
})
export class MainComponent {
	constructor(private httpHelper: HttpHelper) { }
	click()
	{																   
		this.httpHelper.GetAuthorize('/api/sampledata/getdata', {word:"qwe"})
			.subscribe(res =>
			{
				console.log(res.text());
				console.log(res);
			});
		var q = {"word":"qwe"};
		this.httpHelper.PostAuthorize('/api/sampledata/postdata',q)
			.subscribe(res => {
				console.log(res.text());
				console.log(res);
			})
	}
}
