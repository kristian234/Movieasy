import { getServerSession } from "next-auth";
import Logo from "../components/header/logo";
import Search from "../components/header/search";
import LoginButton from "../components/header/login";
import UtilityDropdown from "../components/header/utility-dropdown";
import { getCurrentUser, getLoggedOut } from "../actions/auth-actions";

export default async function Header() {
    const session = await getCurrentUser();
    const error = await getLoggedOut();

    const isAdmin = session?.roles.includes('Admin') ?? false;
    return (
        <nav className="bg-header p-4 flex items-center justify-between shadow-2xl">
            <Logo />
            <Search />
            {session && !error ? (
                <UtilityDropdown isAdmin={isAdmin} />

            ) : (
                < LoginButton />
            )}
        </nav>
    )
}