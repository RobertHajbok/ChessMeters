import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { Contact } from './contact.model';
import { ContactService } from './contact.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html'
})
export class ContactComponent {
  public contact: Contact;

  constructor(private contactService: ContactService, private toastrService: ToastrService, private router: Router) {
    this.contact = {
      firstName: '',
      lastName: '',
      email: '',
      subject: '',
      message: '' 
    };
  }

  public send(): void {
    this.contactService.send(this.contact).subscribe();
    this.toastrService.success('Email successfully sent.');
    this.router.navigateByUrl('/');
  }
}
