interface Props {
    user: any
}

export default function WelcomeGreeting({ user }: Props) {
    return (
        <div className="text-center p-4">
            <h1 className="text-4xl font-bold text-secondary mb-2">
                Welcome{user ? `, ${user.name}` : ''},
            </h1>
            <p className="text-2xl font-semibold text-header-200">
                {user ? "What would you like to watch today?" : "Sign in to start exploring."}
            </p>
        </div>
    )
}
