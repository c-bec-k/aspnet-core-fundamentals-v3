import { Injectable } from '@angular/core';
  import { MatIconRegistry } from '@angular/material/icon';
  import { DomSanitizer } from '@angular/platform-browser';
  @Injectable({
    providedIn: 'root'
  })
  export class AppIconsService {
    constructor(
      private iconRegistry: MatIconRegistry,
      private sanitizer: DomSanitizer
    ) {
        this.iconRegistry.addSvgIcon('maybe', this.sanitizer.bypassSecurityTrustResourceUrl('assets/maybe.svg'));
        this.iconRegistry.addSvgIcon('nope', this.sanitizer.bypassSecurityTrustResourceUrl('assets/nope.svg'));
        this.iconRegistry.addSvgIcon('whoKnows', this.sanitizer.bypassSecurityTrustResourceUrl('assets/whoKnows.svg'));
        this.iconRegistry.addSvgIcon('yes', this.sanitizer.bypassSecurityTrustResourceUrl('assets/yes.svg'));
      }
  }
