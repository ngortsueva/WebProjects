import { NgModule } from '@angular/core';
import { Word } from "./word.model";
import { WordType } from "./wordtype.model";
import { WordCategory } from "./wordcategory.model";
import { WordSubCategory } from "./wordsubcategory.model";
import { Lang } from "./lang.model";



@NgModule({	
	providers: [Word, WordType, WordCategory, WordSubCategory, Lang]
})

export class ModelModule {}
