import { Tabs } from 'expo-router';
import { View } from 'react-native';

export default function TabsLayout() {
  return (
    <View style={{ flex: 1, backgroundColor: '#004a7c' }}>
      <Tabs screenOptions={{
        headerShown: false,
        tabBarStyle: { display: 'none' }
      }}>
        <Tabs.Screen name="Login" />
      </Tabs>
    </View>
  );
} 