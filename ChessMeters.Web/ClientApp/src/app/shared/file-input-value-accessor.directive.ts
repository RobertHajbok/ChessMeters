// Import the core angular services.
import { ControlValueAccessor } from '@angular/forms';
import { Directive } from '@angular/core';
import { ElementRef } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

// ----------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------- //

var noop = () => {
  // ...
};

@Directive({
  selector: 'input[type=file][ngModel][observeFiles]',
  host: {
    '(blur)': 'onTouchedCallback()',
    '(change)': 'handleChange( $event.target.files )'
  },

  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: FileInputValueAccessor,
      multi: true
    }
  ]
})
export class FileInputValueAccessor implements ControlValueAccessor {
  private elementRef: ElementRef;
  private onChangeCallback: Function;
  private onTouchedCallback: Function;

  constructor(elementRef: ElementRef) {
    this.elementRef = elementRef;
    this.onChangeCallback = noop;
    this.onTouchedCallback = noop;

  }

  public handleChange(files: FileList): void {
    if (this.elementRef.nativeElement.multiple) {
      this.onChangeCallback(Array.from(files));
    } else {
      this.onChangeCallback(files.length ? files[0] : null);
    }
  }

  public registerOnChange(callback: Function): void {
    this.onChangeCallback = callback;
  }

  public registerOnTouched(callback: Function): void {
    this.onTouchedCallback = callback;
  }

  public setDisabledState(isDisabled: boolean): void {
    this.elementRef.nativeElement.disabled = isDisabled;
  }

  public writeValue(value: any): void {
    if (value instanceof FileList) {
      this.elementRef.nativeElement.files = value;
    } else if (Array.isArray(value) && !value.length) {
      this.elementRef.nativeElement.files = null;
    } else if (value === null) {
      this.elementRef.nativeElement.files = null;
    }
  }
}
