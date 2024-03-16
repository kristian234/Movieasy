import { getCurrentUser } from "@/app/actions/auth-actions";
import { getRecent, getTrending } from "@/app/actions/movie-actions";
import WelcomeGreeting from "@/app/components/home/welcome-greet";
import MovieCarousel from "@/app/components/movies/movie-carousel";

export default async function HomePage() {
    const user = await getCurrentUser();

    return (
        <div>
            <WelcomeGreeting user={user} />
            <MovieCarousel movies={getRecent} text="Recently Added"></MovieCarousel>
            <br/>
            <MovieCarousel movies={getTrending} text="Trending"></MovieCarousel>
        </div>
    )
}