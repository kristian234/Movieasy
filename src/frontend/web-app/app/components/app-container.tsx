import WelcomeGreeting from "./home/welcome-greet";
import { getCurrentUser } from "../actions/auth-actions";
import MovieCarousel from "./movies/movie-carousel";
import { getRecent } from "../actions/movie-actions";

export default async function AppContainer() {
    const user = await getCurrentUser();
    const movies = await getRecent();

    return (
        <div>
            <MovieCarousel movies={movies} ></MovieCarousel>
            <WelcomeGreeting user={user} />
        </div>
    )
}