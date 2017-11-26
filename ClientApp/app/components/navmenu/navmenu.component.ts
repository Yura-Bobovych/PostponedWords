import { Component } from '@angular/core';
import { LocalStorageService } from './../../services/local-storage.service';
@Component({
	selector: 'nav-menu',
	templateUrl: './navmenu.component.html',
	styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
	constructor(private localStorageService: LocalStorageService) { }
}
