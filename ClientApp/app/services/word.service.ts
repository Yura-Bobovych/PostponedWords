import { Injectable } from '@angular/core';
import { HttpHelper } from './../services/http-helper.service'
import { Word } from './../models/word'
import { Observable } from "rxjs/Observable";
import { Dictionary } from './../models/dictionatry'
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/map'
@Injectable()
export class WordService {
	constructor(private httpHelper: HttpHelper) { }

	
	SearchWord(word: string): Observable<Array<Word>> {
		return this.httpHelper.Get("/api/words/search", { 'Word': word }).map(res => {
			let wordList: Array<Word> = new Array<Word>();
			for (let wordJson of res.json()) {
				let word: Word = new Word(wordJson['wordText'], wordJson['meaning'], wordJson['example'], wordJson['id'])
				wordList.push(word);
			}
			return wordList;
		})
	}
	AddDictionary(dictionaryName: string): void {
		this.httpHelper.PostAuthorize("/api/words/adddictionaty", { 'DictionaryName': dictionaryName }).subscribe();
	}
	AddWordToDictionary(dictionaryId: number, wordId: number): void {
		this.httpHelper.PostAuthorize("/api/words/addwordtodictionary", { "DictionaryId": dictionaryId, "WordId": wordId });
	}
	GetUserDictionaries(): Observable<Array<Dictionary>> {
		return this.httpHelper.GetAuthorize("/api/words/GetUserDictionaries").map(res => {
			let dictinaryList: Array<Dictionary> = new Array<Dictionary>();
			for (let dictJson of res.json())
			{
				let dict: Dictionary = new Dictionary(dictJson['id'], dictJson['name'], dictJson['userId'], dictJson['creationDate']);
				dictinaryList.push(dict);					
			}
			return dictinaryList;
		});
	}
}