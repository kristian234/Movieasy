import WelcomeGreeting from "./home/welcome-greet";
import { getCurrentUser } from "../actions/auth-actions";
import MovieCarousel from "./movies/movie-carousel";
import { getRecent } from "../actions/movie-actions";

export default async function AppContainer() {
    const user = await getCurrentUser();

    return (
        <div>
            <WelcomeGreeting user={user} />
            <MovieCarousel movies={getRecent} text="Recently Added"></MovieCarousel>
        </div>
    )
}