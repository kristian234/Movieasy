import { getCurrentUser } from "../actions/auth-actions";
import LoginButton from "./headerComponents/login";
import Logo from "./headerComponents/logo";
import LogoutButton from "./headerComponents/logout";
import Search from "./headerComponents/search";

export default async function Header() {

    const session = await getCurrentUser();
    return (
        <nav className="bg-header p-4 flex items-center justify-between shadow-2xl">
            <Logo />
            <Search />
            {session ? (
                <LogoutButton />

            ) : (
                < LoginButton />
            )}
        </nav>
    )
}