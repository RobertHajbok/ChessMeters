import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { Contact } from './contact.model';
import { ContactService } from './contact.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html'
})
export class ContactComponent {
  public contact: Contact;

  constructor(private contactService: ContactService, private toastrService: ToastrService) {
    this.contact = {
      firstName: '',
      lastName: '',
      email: '',
      subject: '',
      message: ''
    };
  }

  public send(): void {
    this.contactService.send(this.contact).subscribe(() => {
      this.toastrService.error('Not implemented yet.');
    }, () => {
      this.toastrService.error('An error occurred while trying to submit the form, please try again later.');
    });
  }
}
