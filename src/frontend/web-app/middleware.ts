export { default } from "next-auth/middleware"

export const config = {
    matcher: [
        '/movies/search',
        '/movies/details/:path*'
    ]
}