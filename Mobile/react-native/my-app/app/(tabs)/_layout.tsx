import { Tabs } from 'expo-router';

export default function TabsLayout() {
  return (
    <Tabs screenOptions={{
      headerShown: false,
      tabBarStyle: { display: 'none' }
    }}>
      <Tabs.Screen name="Home" />
      <Tabs.Screen name="Result" />
      <Tabs.Screen name="Profile" />
    </Tabs>
  );
}
