import { withAuth } from "next-auth/middleware"
import { getIsTokenValid } from "./helper";

export default withAuth({
    callbacks: {
        authorized: async ({ token, req }) => {
            if (!token) {
                return false;
            }

            const isTokenValid = getIsTokenValid(token);
            if (!isTokenValid) {
                return false;
            }
            
            return true;
        }
    }
})

