import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { WordService } from './../../services/word.service';
import { Word } from './../../models/word'

@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})
export class SearchComponent {

	constructor(private wordService: WordService) { }
	text: string;
	searchWord: Array<Word>
	onSubmit(form: NgForm) {			
		this.wordService.SearchWord(form.value.search_text).subscribe(
			res => { this.searchWord = res });
		
		this.text = form.value.search_text;
	}
}
