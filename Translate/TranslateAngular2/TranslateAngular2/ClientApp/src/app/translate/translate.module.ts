import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { ModelModule } from "../model/model.module";
import { LangComponent } from "./lang.component";
import { LangService } from "./lang.service";
import { WordTypesComponent } from "./wordtypes.component";

@NgModule({
    declarations: [LangComponent],
    imports: [
      ModelModule,
      BrowserModule,
      FormsModule,
      HttpClientModule
    ],
    providers: [LangService],

  exports: [
    LangComponent,
    WordTypesComponent
  ]
})
export class TranslateModule {}
