import { Component } from '@angular/core';
import { LocalStorageService } from './../../services/local-storage.service';
import { WordService } from './../../services/word.service';
import { Dictionary } from './../../models/dictionatry'
import { NgForm } from "@angular/forms";

@Component({
	selector: 'dictionary-list',
	templateUrl: './dictionary-list.component.html',
	styleUrls: ['./dictionary-list.component.css']
})
export class DictionaryListComponent {
	Dictionaries: Array<Dictionary>
	ngOnInit() {
		this.wordService.GetUserDictionaries().subscribe(res => {

			this.Dictionaries = res;
		}); }
	constructor(private wordService: WordService) { }
	
	onSubmit(form: NgForm) {
		this.wordService.AddDictionary(form.value.search_text);	  
		//this.wordService.GetUserDictionaries().subscribe(res => (this.Dictionaries = res));
	}
	
}
