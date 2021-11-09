import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent implements OnInit {
  @Input() label = '';
  @Input() inputType = '';
  @Input() controlType = 'input';
  @Input() control = new FormControl('').value;
  @Input() runValidation = false;

  constructor() { }

  ngOnInit(): void {
  }

  showErrors() {
    const { dirty, touched, errors } = this.control;
    return dirty && touched && errors;
  }

}
