import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusIcon'
})
export class StatusIconPipe implements PipeTransform {
  transform(value: unknown, ...args: unknown[]): unknown {
    if (value === 'prospect') {
      return 'maybe';
    }
    if (value === 'customer') {
      return 'yes';
    }
    if (value === 'not_interested') {
      return 'nope';
    } else {
      return 'whoKnows';
    }
  }
}
