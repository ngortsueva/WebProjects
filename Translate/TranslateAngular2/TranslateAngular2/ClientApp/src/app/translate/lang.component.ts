import { Component, Inject } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Lang } from "../model/lang.model";

@Component({
  selector: "lang",
  templateUrl: "./lang.component.html"
})

export class LangComponent {
  public langs: Lang[];
  private url = "langs";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string){
    http.get<Lang[]>(baseUrl + this.url).subscribe(result => {
      this.langs = result;
    }, error => console.error(error));
  }
}
