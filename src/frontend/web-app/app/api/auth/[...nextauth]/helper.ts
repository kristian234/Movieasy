import { jwtDecode } from "jwt-decode";
import { JWT } from "next-auth/jwt";

export const getIsTokenValid = (token: JWT) => {
    if (!token) return false;
    if (!token.accessTokenExpiry) return false;

    const dateNowInSeconds = new Date().getTime() / 1000;

    if (dateNowInSeconds > Number(token.accessTokenExpiry)) {
        console.log("API token expired");
        return false;   
     }    //Now we have expiration date of the token 

    return true;
};
 