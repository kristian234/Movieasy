import { getCurrentUser } from "@/app/actions/auth-actions";
import { getRecent, getTrending } from "@/app/actions/movie-actions";
import WelcomeGreeting from "@/app/components/home/welcome-greet";
import MovieCarousel from "@/app/components/movies/movie-carousel";

interface Props {
    user: any
}

export default async function HomePage() {
    const user = await getCurrentUser();

    return (
        <div>
            <WelcomeGreeting user={user} />
            
            <MovieCarousel movies={getTrending} text="Recently Added"></MovieCarousel>
            <MovieCarousel movies={getRecent} text="Trending"></MovieCarousel>
        </div>
    )
}