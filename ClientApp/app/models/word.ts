export class Word
{
	word: string;
	meaning: string;
	example: string;
	id: number;
	constructor(word?: string, meaning?: string, example?: string, id?: number)
	{
		this.word = word ? word : "";
		this.meaning = meaning ? meaning : "";
		this.example = example ? example : "";
		this.id = id ? id : -1;
	}
}