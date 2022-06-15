import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { LayoutState, toggleSidenav } from './store/layout.store';

@Component({
  selector: 'crm-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private store: Store<LayoutState>){}
  sideNavToggle() {
    this.store.dispatch(toggleSidenav());
  }

  title = 'Simple CRM';
}
