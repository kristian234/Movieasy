import { withAuth } from "next-auth/middleware"

export default withAuth(
  // `withAuth` augments your `Request` with the user's token.
  {
    callbacks: {
      authorized: ({ req, token }) => {

        if (req.nextUrl.pathname.startsWith('/admin')) {
          return token?.croles?.includes('Admin') ?? false;
        }

        if (token?.error) {
          return false;
        }

        return Boolean(token?.accessToken);
      }
    },
  }
)
export const config = {
  matcher: [
    '/movies/search',
    '/home',
    '/movies/details/:path*',
    '/admin/:path*'
  ]
}