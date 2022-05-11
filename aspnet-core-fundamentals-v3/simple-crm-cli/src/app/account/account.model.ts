export interface UserSummaryViewModel {
  id: string;
  name: string;
  emailAddress: string;
  jwt: string;
  roles: string[];
}

export interface MicrosoftOptions {
  client_id: string;
  scope: string;
  state: string;
}

export interface GoogleOptions {

}

export const anonymousUser = (): UserSummaryViewModel => ({
    name: "Anonymous",
    id: "",
    emailAddress: "",
    jwt: "",
    roles: [],
  });
