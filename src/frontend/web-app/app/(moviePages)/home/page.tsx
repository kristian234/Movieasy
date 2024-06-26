import SignalRComponent from "@/app/Providers/SignalRProvider";
import { getCurrentUser, getTokenWorkaround } from "@/app/actions/auth-actions";
import { getRecentlyUploaded, getTrending } from "@/app/actions/movie-actions";
import WelcomeGreeting from "@/app/components/home/welcome-greet";
import MovieCarousel from "@/app/components/movies/movie-carousel";

export default async function HomePage() {
    const user = await getCurrentUser();
    const jwtToken = await getTokenWorkaround();
    return (
        <div>
            {jwtToken?.accessToken && (
                <SignalRComponent jwt={jwtToken?.accessToken} />
            )}
            <WelcomeGreeting user={user} />

            <MovieCarousel movies={getRecentlyUploaded} text="Recently Added"></MovieCarousel>
            <MovieCarousel movies={getTrending} text="You may have already watched"></MovieCarousel>
        </div>
    )
}