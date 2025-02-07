import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { router } from 'expo-router';

type IconName = 'home' | 'person';

const Footer = () => {
  const footerItems: { icon: IconName; label: string }[] = [
    { icon: 'home', label: 'Trang chủ' },
    { icon: 'person', label: 'Hồ sơ' },
  ];

  const handleFooterPress = (route: string) => {
    if (route === 'home') {
      router.push('/(tabs)/Home');
    } else if (route === 'person') {
      router.push('/(tabs)/Profile');
    }
  };

  return (
    <View style={styles.footer}>
      {footerItems.map((item, index) => (
        <TouchableOpacity
          key={index}
          style={styles.footerItem}
          onPress={() => handleFooterPress(item.icon)}
        >
          <Ionicons name={item.icon} size={24} color="#0000cc" />
          <Text style={styles.footerText}>{item.label}</Text>
        </TouchableOpacity>
      ))}
    </View>
  );
};

const styles = StyleSheet.create({
  footer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    backgroundColor: 'white',
    paddingVertical: 10,
    borderTopWidth: 1,
    borderTopColor: '#ddd',
    position: 'absolute',
    bottom: 0,
    left: 0,
    right: 0,
  },
  footerItem: {
    alignItems: 'center',
  },
  footerText: {
    marginTop: 5,
    fontSize: 12,
    color: '#0000cc',
  },
});

export default Footer;