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

export const anonymousUser = (): UserSummaryViewModel => ({
    name: "Anonymous",
    id: "nil",
    emailAddress: "none@email.no",
    jwt: Math.random().toString(16).slice(2),
    roles: [],
  });
