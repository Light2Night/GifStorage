import { Home } from "./components/Home";
import { FetchData } from "./components/FetchData";
import { GifList } from "./components/GifList";
import AddGif from "./components/AddGif";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/gif-list',
    element: <GifList />
  },
  {
    path: '/add-gif',
    element: <AddGif />
  }
];

export default AppRoutes;
