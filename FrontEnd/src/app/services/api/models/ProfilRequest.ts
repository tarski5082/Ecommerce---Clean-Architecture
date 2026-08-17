import { AddressRequest } from "./AdresseRequest";

export interface UserRequest {
  username: string;
  nom?: string;
  prenom?: string;
  facturation?: AddressRequest | null;
  livraison?: AddressRequest | null;
}