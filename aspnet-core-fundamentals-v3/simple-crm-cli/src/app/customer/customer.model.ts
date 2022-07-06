export type InteractionMethod = 'phone' | 'email';
export type StatusCode = 'not_interested' | 'customer' | 'unknown' | 'prospect';

export interface Customer {
  id: number;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  emailAddress: string;
  preferredContactMethod: InteractionMethod;
  statusCode: StatusCode;
  lastContactDate: string; //ISO-formatted date string

}
