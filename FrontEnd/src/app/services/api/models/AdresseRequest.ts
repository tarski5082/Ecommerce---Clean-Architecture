import { LocalityRequest } from "./Localite";

export interface AddressRequest {
  rue: string;
  numero: number;
  boite?: string | null;
  localite: LocalityRequest;
}