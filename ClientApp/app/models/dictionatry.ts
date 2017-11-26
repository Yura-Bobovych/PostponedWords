export class Dictionary
{
	public id: number
	public dictionaryName: string
	public userId: number;
	public creationDate: Date;
	constructor(id: number, dictionaryName: string, userId: number, creationDate: Date)
	{
		this.id = id;
		this.dictionaryName = dictionaryName;
		this.userId = userId;
		this.creationDate = creationDate;
	}

}