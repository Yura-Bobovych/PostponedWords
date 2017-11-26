import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { MainComponent } from './components/main/main.component';
import { SearchComponent } from './components/search/search.component';
import { SearchWordComponent } from './components/search-word/search-word.component';
import { DictionaryListComponent } from './components/dictionary-list/dictionary-list.component';

import { AuthGuard } from './services/auth-guard.service';
import { LocalStorageService } from './services/local-storage.service';
import { HttpHelper } from './services/http-helper.service';
import { WordService } from './services/word.service';

import { LocalStorage } from "./models/local-storage";

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		CounterComponent,
		FetchDataComponent,
		HomeComponent,
		SignInComponent,
		MainComponent,
		SearchComponent,
		SearchWordComponent,
		DictionaryListComponent
	],
	imports: [
		CommonModule,
		HttpModule,
		FormsModule,
		RouterModule.forRoot([
			{ path: '', redirectTo: 'home', pathMatch: 'full' },
			{ path: 'main', component: MainComponent },
			{ path: 'home', component: HomeComponent },
			{ path: 'counter', component: CounterComponent, canActivate: [AuthGuard] },
			{ path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard] },
			{ path: '**', redirectTo: 'home' }
		])

	],
	providers: [AuthGuard, LocalStorageService, HttpHelper, WordService,
		{ provide: LocalStorage, useValue: { getItem() { } } }]
})
export class AppModuleShared {
}
