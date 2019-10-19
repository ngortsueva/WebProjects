import { Component, Inject } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { WordType } from "../model/wordtype.model";

@Component({
  selector: "word-types",
  templateUrl: "./wordtypes.component.html"
})

export class WordTypesComponent {
  public wordtypes: WordType[];
  private url = "wordtypes";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WordType[]>(baseUrl + this.url).subscribe(result => {
      this.wordtypes = result;
    }, error => console.error(error));
  }
}
