import Body from "../layout/body";
import Footer from "../layout/footer";
import Header from "../layout/header";

export default function AppContainer() {
    return (
        <div className="min-h-screen flex-auto max-h-screen overflow-auto">
            <Header />

            <Body />
        </div>
    )
}