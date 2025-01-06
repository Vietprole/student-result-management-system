import React, { useState } from 'react';
import { Alert, FlatList, ScrollView, StyleSheet, Text, TouchableOpacity, View, SafeAreaView, Platform, StatusBar, Image } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { router } from 'expo-router';
import { SelectList } from 'react-native-dropdown-select-list';

const handleFooterPress = (route: string) => {
  if (route === 'home') {
    router.push('/(tabs)/Home');
  } else if (route === 'person') {
    router.push('/(tabs)/Profile');
  }
};

type TableRow = {
  TT: string;
  Name: string;
  Description: string;
  Department: string;
};

type ColumnWidths = {
  [key: string]: number;
};

const Result = () => {
  const tableHeaders = ['TT', 'Tên', 'Mô tả', 'Ngành'];
  const tableData: TableRow[] = [
    { TT: '1', Name: 'PLO 1', Description: 'Kỹ năng ngoại ngữ', Department: 'CNTT K2021'},
    { TT: '2', Name: 'PLO 2', Description: 'Kỹ năng làm việc nhóm', Department: 'CNTT K2021 Nhật'},
    { TT: '3', Name: 'PLO 3', Description: 'Kỹ năng chuyên ngành', Department: 'CNTT K2021'},
    { TT: '4', Name: 'PLO 4', Description: 'Kỹ năng ngoại ngữ', Department: 'CNTT K2021'},
    { TT: '5', Name: 'PLO 5', Description: 'Kỹ năng ngoại ngữ 1', Department: 'CNTT K2021'},
    { TT: '6', Name: 'PLO 6', Description: 'Kỹ năng làm việc nhóm', Department: 'CNTT K2021'},

  ];
  const columnWidths: ColumnWidths = {
    TT: 40,
    Name: 160,
    Description: 200,
    Department: 160,
  };

  const headerToWidthKey: { [key: string]: string } = {
    'TT': 'TT',
    'Tên': 'Name',
    'Mô tả': 'Description',
    'Ngành': 'Department'
  };

  const renderHeader = () => (
    <View style={[styles.row, styles.headerRow]}>
      {tableHeaders.map((header, index) => (
        <Text
          key={index}
          style={[
            styles.cell,
            styles.headerCell,
            { width: columnWidths[headerToWidthKey[header]] }
          ]}
        >
          {header}
        </Text>
      ))}
    </View>
  );

  const renderRow = ({ item, index }: { item: TableRow; index: number }) => (
    <View style={[styles.row, index % 2 === 0 ? styles.evenRow : styles.oddRow]}>
      <Text style={[styles.cell, { width: columnWidths.TT }]}>{item.TT}</Text>
      <Text style={[styles.cell, { width: columnWidths.Name }]}>{item.Name}</Text>
      <Text style={[styles.cell, { width: columnWidths.Description }]}>{item.Description}</Text>
      <Text style={[styles.cell, { width: columnWidths.Department }]}>{item.Department}</Text>

    </View>
  );

  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="light-content" backgroundColor="#0000cc" />
      <View style={styles.container}>
        {/* Header */}
        <View style={styles.header}>
          <View style={styles.headerLeft}>
            <Image
              source={{ uri: 'https://i.imghippo.com/files/mUo4100yA.webp' }}
              style={styles.headerLogo}
            />
          </View>
          <Text style={styles.logoText}>THỐNG KÊ PLO</Text>
          <Ionicons name="notifications-outline" size={24} color="white" />
        </View>        

        {/* Table Content */}
        <ScrollView horizontal>
          <View style={styles.table}>
            {renderHeader()}
            <FlatList
              data={tableData}
              renderItem={renderRow}
              keyExtractor={(item, index) => index.toString()}
            />
          </View>
        </ScrollView>

        {/* Footer */}
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
      </View>
    </SafeAreaView>
  );
};

const footerItems: { icon: 'home' | 'person', label: string }[] = [
  { icon: 'home', label: 'Trang chủ' },
  { icon: 'person', label: 'Hồ sơ' },
];

const styles = StyleSheet.create({
  table: {
    borderWidth: 1,
    borderColor: '#ccc',
    marginHorizontal: 16,
    borderRadius: 8,
    overflow: 'hidden',
    margin: 16,
  },
  row: {
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
  },
  cell: {
    padding: 10,
    textAlign: 'center',
    borderRightWidth: 1,
    borderRightColor: '#ccc',
    justifyContent: 'center',
  },
  headerRow: {
    backgroundColor: '#f1f1f1',
    borderBottomWidth: 2,
    borderBottomColor: '#ccc',
  },
  headerCell: {
    fontWeight: 'bold',
    fontSize: 14,
    backgroundColor: '#f1f1f1',
    textAlign:'center'
  },
  evenRow: {
    backgroundColor: '#f9f9f9',
  },
  oddRow: {
    backgroundColor: '#ffffff',
  },
  link: {
    color: 'blue',
    textDecorationLine: 'underline',
    textAlign: 'center',
  },
  safeArea: {
    flex: 1,
    backgroundColor: '#000',
    paddingTop: Platform.OS === 'android' ? StatusBar.currentHeight : 0,
  },
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#0000cc',
    padding: 15,
    height: 70,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerLogo: {
    width: 50,
    height: 50,
    marginRight: 10,
  },
  logoText: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
  },
  footer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    backgroundColor: 'white',
    paddingVertical: 10,
    borderTopWidth: 1,
    borderTopColor: '#ddd',
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

export default Result;