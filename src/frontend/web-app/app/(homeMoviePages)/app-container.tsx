import { GetServerSideProps } from 'next';
import { getCurrentUser } from '../actions/auth-actions';
import HomePage from './home/home-page';

export default async function AppContainer() {
    const user = await getCurrentUser();

    if (user) {
        return <HomePage />
    }

    return (
        <div>
            <h1>Normal page for non-logged in</h1>
        </div>
    )
}

