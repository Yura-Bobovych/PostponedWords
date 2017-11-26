import { Component, Input } from '@angular/core';

import { Word } from './../../models/word';

@Component({
	selector: 'search-word',
	templateUrl: './search-word.component.html',
	styleUrls: ['./search-word.component.css']
})
export class SearchWordComponent {
	@Input() word: Word;
	Add()
	{
		
		alert('added');
	}
}
