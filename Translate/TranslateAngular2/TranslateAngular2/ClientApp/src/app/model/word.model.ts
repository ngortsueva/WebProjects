import { WordType } from "./wordtype.model";
import { WordCategory } from "./wordcategory.model";

export class Word {
  public id: number;
  public wordEng: string;
  public wordRu: string;
  public wordType: WordType;
  public wordCat: WordCategory;
}
