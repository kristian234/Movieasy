import { getServerSession } from "next-auth";
import Logo from "../components/header/logo";
import Search from "../components/header/search";
import LoginButton from "../components/header/login";
import UtilityDropdown from "../components/header/utility-dropdown";
import { getSession } from "../actions/auth-actions";
import { Fragment } from "react";

export default async function Header() {
    const session = await getSession();

    const isAdmin = session?.user.roles?.includes('Admin') ?? false;
    return (
        <nav className="bg-header p-4 flex items-center justify-between shadow-2xl">
            <Logo />
            {session && !session.error ? (
                <Fragment>
                    <Search />
                    <UtilityDropdown isAdmin={isAdmin} />
                </Fragment>
            ) : (
                < LoginButton />
            )}
        </nav>
    )
}