'use client'

interface Props {
    user: any
}

export default async function WelcomeGreeting({ user }: Props) {
    if (user) {
        return (
            <h1>
                Welcome, {user.name}
            </h1>
        )
    } else {
        return (
            <h1>
                Welcome,
            </h1>
        )
    }
}
