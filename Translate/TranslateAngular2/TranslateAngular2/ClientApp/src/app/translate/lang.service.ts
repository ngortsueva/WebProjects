import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Lang } from "../model/lang.model";

@Injectable()
export class LangService {
  private url_langs: string;

  constructor(private http: HttpClient) {
    this.url_langs = "https://localhost:5001/api/langs";
  }

  getLangs(){

  }
}
