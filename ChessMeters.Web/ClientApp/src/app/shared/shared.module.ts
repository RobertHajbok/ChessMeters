import { NgModule } from '@angular/core';

import { FileInputValueAccessor } from "./file-input-value-accessor.directive";

@NgModule({
  declarations: [FileInputValueAccessor],
  exports: [FileInputValueAccessor]
})
export class SharedModule {
}
