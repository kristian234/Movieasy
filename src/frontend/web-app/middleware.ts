import { withAuth } from "next-auth/middleware"

export default withAuth(
    // `withAuth` augments your `Request` with the user's token.
    {
      callbacks: {
        authorized: ({ req, token }) => {

            if(req.nextUrl.pathname.startsWith('/admin')){
                return token?.croles?.includes('Admin') ?? false;
            }
            
            return Boolean(token);
        }
      },
    }
  )
export const config = {
    matcher: [
        '/movies/search',
        '/movies/details/:path*',
        '/admin/:path*'
    ]
}